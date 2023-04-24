import {FC} from "react";
import {NodeProps} from "@reactflow/core/dist/esm/types/nodes";
import {Handle, Position} from "reactflow";
import {Styled} from "./Style.tsx";
import {GraphElementsStyled} from "../GraphElementsStyled.ts";
import {symbols} from "../../symbols.tsx";
import {ActivationMode, GraphNode, PullMode} from "../../../../common/types";

export const FlowNodePool : FC<NodeProps> = ({ data, isConnectable, type, selected }) => {
  const { node } = data;
  const pool = node as GraphNode;
  let pushLabel = '';
  if (pool.pullMode === PullMode.PushAny || pool.pullMode === PullMode.PushAll)
    pushLabel = 'p';
  if (pool.pullMode === PullMode.PullAll || pool.pullMode === PullMode.PushAll)
    pushLabel += '&';

  return (
    <Styled.Root>
      <Handle
        type="target"
        position={Position.Left}
        style={{ background: '#555', left: 0.05, zIndex: 10 }}
        onConnect={(params) => console.log('handle onConnect', params)}
        isConnectable={isConnectable}
      />
      <GraphElementsStyled.ElementWrapper selected={selected}>
        {symbols[type]}
      </GraphElementsStyled.ElementWrapper>
      <Styled.AmountLabel>{pool.state?.currentAmount || 0}</Styled.AmountLabel>
      <Styled.NameLabel>{pool.name}</Styled.NameLabel>
      {pushLabel ? <Styled.PushLabel>{pushLabel}</Styled.PushLabel> : null}
      {(pool.activationMode === ActivationMode.Auto ? <Styled.AutoLabel>*</Styled.AutoLabel> :
        pool.activationMode === ActivationMode.OnStart ? <Styled.OnStartLabel>s</Styled.OnStartLabel> : null)}
      <Handle
        type="source"
        position={Position.Right}
        id="a"
        style={{ background: '#555', right: -0.9, zIndex: 10 }}
        isConnectable={isConnectable}
      />
    </Styled.Root>
  );
}
