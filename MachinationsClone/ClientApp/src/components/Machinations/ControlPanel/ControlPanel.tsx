import {Button, FormControl, MenuItem, TextField, Typography} from '@mui/material';
import { Styled } from './Style';
import {ActivationMode, GraphConnection, GraphElement, GraphNode, PullMode} from "../../../common/types";
import React, {FC, useState} from "react";
import {useApi} from "../../../common";
import {PoolEdit} from "./components/PoolEdit/PoolEdit.tsx";
import {ResourceConnectionEdit} from "./components/ResourceConnectionEdit/ResourceConnectionEdit.tsx";

export type ControlPanelProps = {
  selected: GraphElement | null;
  onNodeUpdate: (node: GraphNode) => void;
  onConnectionUpdate: (connection: GraphConnection) => void;
}

export type EditComponentProps = {
  element: GraphElement;
  properties: Record<string, string>;
  setProperties: React.Dispatch<React.SetStateAction<Record<string, string>>>;
}

const EditComponentMap: {node: Record<string, FC<EditComponentProps>>, connection: Record<string, FC<EditComponentProps>>} = {
  node: {
    pool: PoolEdit,
    source: () => null,
  },
  connection: {
    resourceConnection: ResourceConnectionEdit
  }
}

const camelToTitle = (camel: string) => {
  return camel.replace(/([A-Z])/g, ' $1').replace(/^./, function(str){ return str.toUpperCase(); })
}

export const ControlPanel = ({ selected, onNodeUpdate, onConnectionUpdate }: ControlPanelProps) => {
  let editComponent = null;
  let nodeEdit = null;
  let connectionEdit = null;
  const [update, setUpdate] = useState<any>({});
  const [properties, setProperties] = useState<Record<string, string>>({});
  const { post } = useApi('/graph');

  if (selected) {
    if (selected.elementType === 'node') {
      const node = selected as GraphNode;
      editComponent = EditComponentMap.node[node.nodeType];

      const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        node.name = event.target.value;
        setUpdate((update: any) => ({...update, name: node.name}));
      }

      const handleActivationModeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        node.activationMode = event.target.value as ActivationMode;
        setUpdate((update: any) => ({...update, activationMode: node.activationMode}));
      }

      const handlePullModeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        node.pullMode = event.target.value as PullMode;
        setUpdate((update: any) => ({...update, pullMode: node.pullMode}));
      }

      nodeEdit = (
        <>
          <Typography variant="h5" component="h2">
            {camelToTitle(node.nodeType)}
          </Typography>
          <FormControl fullWidth margin="normal">
            <TextField required id="name" label="Name" value={update.name || node.name}
                       onChange={handleNameChange} />
          </FormControl>
          <FormControl fullWidth margin="normal">
            <TextField
              select
              value={update.activationType || node.activationMode}
              label="Activation Type"
              onChange={handleActivationModeChange}
              variant="outlined"
              size="small"
            >
              <MenuItem value={ActivationMode.Auto}>Auto</MenuItem>
              <MenuItem value={ActivationMode.Passive}>Passive</MenuItem>
              <MenuItem value={ActivationMode.OnStart}>On Start</MenuItem>
            </TextField>
          </FormControl>
          <FormControl fullWidth margin="normal">
            <TextField
              select
              value={update.pullMode || node.pullMode}
              label="Pull Mode"
              onChange={handlePullModeChange}
              variant="outlined"
              size="small"
            >
              <MenuItem value={PullMode.PullAny}>Pull Any</MenuItem>
              <MenuItem value={PullMode.PullAll}>Pull All</MenuItem>
              <MenuItem value={PullMode.PushAny}>Push Any</MenuItem>
              <MenuItem value={PullMode.PushAll}>Push All</MenuItem>
            </TextField>
          </FormControl>
        </>
      )
    }
    else if (selected.elementType === 'connection') {
      const connection = selected as GraphConnection;
      editComponent = EditComponentMap.connection[connection.connectionType];
      connectionEdit = (
        <Typography variant="h5" component="h2">
          {camelToTitle(connection.connectionType)}
        </Typography>
      )
    }
  }

  const onSave = () => {
    if (selected?.elementType === 'node') {
      post(`/${selected.graphId}/updateNode`, null, {
        ...update,
        id: selected.id,
        properties
      })
        .then(res => res.json())
        .then((node: GraphNode) => {
          onNodeUpdate(node);
        })
    }
    else if (selected?.elementType === 'connection') {
      post(`/${selected.graphId}/updateConnection`, null, {
        ...update,
        id: selected.id,
        properties
      })
        .then(res => res.json())
        .then((connection: GraphConnection) => {
          onConnectionUpdate(connection);
        })
    }
  }

  return (
    <Styled.Root>
      {selected && selected.elementType === 'node' && nodeEdit}
      {selected && selected.elementType === 'connection' && connectionEdit}
      {editComponent && selected ? editComponent({element: selected, setProperties, properties}) : null}
      <Styled.ButtonWrapper>
        <Button
          disabled={!selected}
          variant="outlined"
          onClick={onSave}
        >
          Save
        </Button>
      </Styled.ButtonWrapper>
    </Styled.Root>
  );
};
