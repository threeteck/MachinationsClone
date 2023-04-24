import React from 'react';
import {Typography} from '@mui/material';
import { Styled } from './Style';
import {useElementTypes} from "../../App";
import {symbols} from "../symbols";

export type ElementsBarProps = {
  onNodeSelected?: (nodeType: string) => void;
  onConnectionSelected?: (connectionType: string) => void;
}

export const ElementsBar = () => {
  // console.log(selected)
  const elementTypes = useElementTypes();
  
  if (!elementTypes.isLoaded) return null;

  const onDragStart = (event: React.DragEvent<HTMLDivElement>, nodeType: string) => {
    event.dataTransfer.setData('application/reactflow', nodeType);
    event.dataTransfer.effectAllowed = 'move';
  };
  
  return (
    <Styled.Wrapper>
      <Styled.Root>
        <Typography variant="h5" component="h2">
          Nodes
        </Typography>
        <Styled.ElementsBarWrapper>
          {Object.values(elementTypes.nodeTypes).map((nodeType: any) => (
            <Styled.ElementWrapper key={nodeType.name} onDragStart={(event) => onDragStart(event, nodeType.name)} draggable>
              {symbols[nodeType.name]}
            </Styled.ElementWrapper>
          ))}
        </Styled.ElementsBarWrapper>
      </Styled.Root>
      <Styled.Root>
        <Typography variant="h5" component="h2">
          Connections
        </Typography>
        <Styled.ElementsBarWrapper>
          {Object.values(elementTypes.connectionTypes).map((connectionType: any) => (
            <Styled.ElementWrapper key={connectionType.name}>
              {symbols[connectionType.name]}
            </Styled.ElementWrapper>
          ))}
        </Styled.ElementsBarWrapper>
      </Styled.Root>
    </Styled.Wrapper>
  );
};
