import {useCallback, useEffect, useRef, useState} from 'react';
import {useParams} from "react-router-dom";
import ReactFlow, {
  Node,
  Edge,
  useNodesState,
  useEdgesState,
  MarkerType,
  ReactFlowInstance,
  Connection, NodeChange, Controls, EdgeChange
} from 'reactflow';
import 'reactflow/dist/style.css';
import { Styled } from './Style';
import { ControlPanel } from './ControlPanel';
import {GraphConnection, ConnectionType, GraphElement, GraphNode, NodeGeometry, NodeType} from "../../common/types";
import {useApi, useFetch} from "../../common";
import {ElementTypesData, useElementTypes} from "../App";
import {ElementsBar} from "./ElementsBar/ElementsBar";
import {FlowNodePool} from "./GraphElements/FlowNodePool";
import {NodeTypes} from "@reactflow/core/dist/esm/types/general";
import {FlowNodeSource} from "./GraphElements/FlowNodeSource";

const flowNodeTypes: NodeTypes = {
  pool: FlowNodePool,
  source: FlowNodeSource
}

const createNode = (node: GraphNode, nodeGeometry: {x: number, y: number}, types: ElementTypesData): Node => {
  const nodeType = types.nodeTypes[node.nodeType];
  return {
    id: node.id,
    type: nodeType.name,
    position: { x: nodeGeometry.x, y: nodeGeometry.y },
    data: { label: node.name, node },
  }
}

const createConnection = (connection: GraphConnection, types: ElementTypesData): Edge => {
  const connectionType = types.connectionTypes[connection.connectionType];
  return {
    id: connection.id,
    source: connection.startId,
    target: connection.endId,
    type: 'smoothstep', // connectionType.name,
    markerEnd: {
      type: MarkerType.ArrowClosed,
    },
    // TODO: remove this hack and create custom edge component for resource connections
    label: (connection.connectionType === 'resourceConnection' ? connection.properties.speed : undefined),
    data: { connection }
  }
}

const createElements = (graph: Map<string, GraphElement>, nodeGeometryMap: Map<string, NodeGeometry>, types: ElementTypesData): [Node[], Edge[]] => {
  const nodes: Node[] = [];
  const connections: Edge[] = [];
  for (const [key, value] of graph) {
    if (value.elementType === 'node') {
      const node = value as GraphNode;
      const nodeGeometry = nodeGeometryMap.get(key);
      if (nodeGeometry === undefined) {
        console.log('nodeGeometry is undefined')
        continue;
      }
      nodes.push(createNode(node, nodeGeometry, types));
    } else {
      const connection = value as GraphConnection;
      connections.push(createConnection(connection, types));
    }
  }
  
  return [nodes, connections];
}

export const Machinations = () => {
  const [selected, setSelected] = useState<GraphElement | null>(null);
  const types = useElementTypes();
  const [isLoaded, setIsLoaded] = useState(types.isLoaded);
  
  const { get: getNodeTypes } = useApi('/graph');
  const { get: getConnectionTypes } = useApi('/graph');
  const { data: graph, get: getGraph } = useFetch('/graph');
  const { data: nodeGeometry, get: getNodeGeometry } = useFetch('/graph');

  const { id: graphId } = useParams<{ id: string }>();
  const { post: addNode } = useApi(`/graph/${graphId}/addNode`);
  const { post: addConnection } = useApi(`/graph/${graphId}/addConnection`);
  // const { post: updateNode } = useApi(`/graph/${graphId}/updateNode`);
  // const { post: updateConnection } = useApi(`/graph/${graphId}/updateConnection`);
  const { post: updateNodeGeometry } = useApi(`/graph/${graphId}/updateNodeGeometry`);
  const { post: deleteNode } = useApi(`/graph/${graphId}/deleteNode`);
  const { post: deleteConnection } = useApi(`/graph/${graphId}/deleteConnection`);
    
  const flowWrapper = useRef<HTMLDivElement>(null); 
  if (!types.isLoaded) {
    getNodeTypes('/nodeTypes')
      .then(res => res.json())
      .then((data: [NodeType]) => {
      for (const type of data) {
        types.nodeTypes[type.name] = type;
      }
    })
      .then(() => getConnectionTypes('/connectionTypes'))
      .then(res => res.json())
      .then((data: [ConnectionType]) => {
        for (const type of data) {
          types.connectionTypes[type.name] = type;
        }
      })
      .then(() => {
        types.isLoaded = true;
        setIsLoaded(true);
        console.log(types)
      });
  }
  
  useEffect(() => {
    console.log('trying to load graph')
    if (!isLoaded) return;
    console.log('loading graph')
    getGraph(`/${graphId}`);
    getNodeGeometry(`/${graphId}/geometry`);
  }, [getGraph, getNodeGeometry, graphId, isLoaded]);
  
  const [elementsMap, setElementsMap] = useState<Map<string, GraphElement>>(new Map<string, GraphElement>());
  const [nodeGeometryMap, setNodeGeometryMap] = useState<Map<string, NodeGeometry>>(new Map<string, NodeGeometry>());
  
  useEffect(() => {
    if (nodeGeometry === null || nodeGeometry === undefined) return;
    const map = new Map<string, NodeGeometry>();
    for (const geometry of nodeGeometry) {
      map.set(geometry.nodeId, geometry);
    }
    setNodeGeometryMap(map);
  }, [nodeGeometry]);
  
  useEffect(() => {
    if (graph === null || graph === undefined) return;
    const map = new Map<string, GraphElement>();
    for (const element of graph.graphElements) {
      map.set(element.id, element);
    }
    setElementsMap(map);
  }, [graph]);
  
  const initNodes: Node[] = [];
  const initConnections: Edge[] = [];
  
  const [nodes, setNodes, onNodesChange] = useNodesState(initNodes);
  const [edges, setEdges, onEdgesChange] = useEdgesState(initConnections);
  const [reactFlowInstance, setReactFlowInstance] = useState<ReactFlowInstance | null>(null);

  useEffect(() => {
    if (!isLoaded || elementsMap.size === 0 || nodeGeometryMap.size === 0) return;
    const [nodes, connections] = createElements(elementsMap, nodeGeometryMap, types);
    setNodes(nodes);
    setEdges(connections);
  }, [isLoaded, elementsMap, nodeGeometryMap, types, setNodes, setEdges]);

  const onDragOver = useCallback((event: any) => {
    event.preventDefault();
    event.dataTransfer.dropEffect = 'move';
  }, []);
  
  const onDrop = useCallback((event: any) => {
    event.preventDefault();

    if (!flowWrapper.current) return;
    const reactFlowBounds = flowWrapper.current.getBoundingClientRect();
    const type = event.dataTransfer.getData('application/reactflow');

    if (typeof type === 'undefined' || !type) {
      return;
    }

    if (!reactFlowInstance) return;

    const position = reactFlowInstance.project({
      x: event.clientX - reactFlowBounds.left,
      y: event.clientY - reactFlowBounds.top,
    });
    
    const typeCount = nodes.filter(node => node.type === type).length;
    
    const newNode = {
      name: `${type} ${typeCount}`,
      nodeTypeName: type,
      x: position.x,
      y: position.y,
    };
    
    addNode(null, null, newNode).then(res => res.json())
      .then((data: GraphNode) => {
        const node = createNode(data, data.geometry!, types);
        setNodes((nodes) => nodes.concat(node));
      });
  }, [reactFlowInstance, nodes, addNode, types, setNodes]);
  
  const onConnect = useCallback((params: Connection) => {
    const newConnection = {
      connectionTypeName: 'resourceConnection',
      startId: params.source,
      endId: params.target,
    };
    
    addConnection(null, null, newConnection).then(res => res.json())
      .then((data: GraphConnection) => {
        const connection = createConnection(data, types);
        setEdges((edges) => edges.concat(connection));
      });
  }, [addConnection, setEdges, types]);
  
  const onNodeChangeHandler = useCallback((nodeChanges: NodeChange[]) => {
    for (const nodeChange of nodeChanges) {
      if (nodeChange.type === 'position' && nodeChange.dragging === false) {
        const node = nodes.find(node => node.id === nodeChange.id);
        if (node !== undefined) {
          const update = {
            nodeId: nodeChange.id,
            x: node.position.x,
            y: node.position.y,
            dragging: nodeChange.dragging,
          };
          updateNodeGeometry(null, null, update);
          console.log('updating node geometry');
        }
      }
      if (nodeChange.type === 'select') {
        const node = nodes.find(node => node.id === nodeChange.id);
        if (node !== undefined) {
          if (nodeChange.selected) {
            setSelected(elementsMap.get(nodeChange.id)!);
          }
        }
      }
    }
    
    onNodesChange(nodeChanges);
  }, [onNodesChange, nodes, updateNodeGeometry, elementsMap]);

  const onEdgeChangeHandler = useCallback((edgeChanges: EdgeChange[]) => {
    for (const edgeChange of edgeChanges) {
      if (edgeChange.type === 'select') {
        const edge = edges.find(edge => edge.id === edgeChange.id);
        if (edge !== undefined) {
          if (edgeChange.selected) {
            setSelected(elementsMap.get(edgeChange.id)!);
          }
        }
      }
    }

    onEdgesChange(edgeChanges);
  }, [onEdgesChange, edges, elementsMap]);
  
  const onNodeDelete = useCallback((deleted: Node[]) => {
    for (const node of deleted) {
      deleteNode(null, null, { nodeId: node.id }).then(res => res.json())
        .then((data: GraphNode) => {
          setNodes((nodes) => nodes.filter(node => node.id !== data.id));
        });
    }
  }, [deleteNode, setNodes]);
  
  const onEdgeDelete = useCallback((deleted: Edge[]) => {
    for (const edge of deleted) {
      deleteConnection(null, null, { connectionId: edge.id })
        .then(res => res.json())
        .then((data: GraphConnection) => {
          setEdges((edges) => edges.filter(edge => edge.id !== data.id));
        });
    }
  }, [deleteConnection, setEdges]);

  const onNodeUpdate = useCallback((node: GraphNode) => {
    elementsMap.set(node.id, node);
    setNodes((nodes) => nodes.map(n => n.id === node.id ? createNode(node, {
      x: n.position.x,
      y: n.position.y,
    }, types) : n));
  }, [elementsMap, setNodes, types]);

  const onConnectionUpdate = useCallback((connection: GraphConnection) => {
    elementsMap.set(connection.id, connection);
    setEdges((edges) => edges.map(e => e.id === connection.id ? createConnection(connection, types) : e));
  }, [elementsMap, setEdges, types]);

  return (
    <Styled.Root>
      <Styled.MainArea>
        <Styled.FlowWrapper ref={flowWrapper}>
          <ReactFlow
            nodes={nodes}
            edges={edges}
            onNodesChange={onNodeChangeHandler}
            onEdgesChange={onEdgeChangeHandler}
            onConnect={onConnect}
            onInit={setReactFlowInstance}
            onDrop={onDrop}
            onDragOver={onDragOver}
            onNodesDelete={onNodeDelete}
            onEdgesDelete={onEdgeDelete}
            nodeTypes={flowNodeTypes}
            >
            <Controls />
          </ReactFlow>
        </Styled.FlowWrapper>
      </Styled.MainArea>
      <Styled.ControlPanelWrapper>
        <ControlPanel selected={selected} onNodeUpdate={onNodeUpdate} onConnectionUpdate={onConnectionUpdate} />
        <ElementsBar />
      </Styled.ControlPanelWrapper>
    </Styled.Root>
  );
};
