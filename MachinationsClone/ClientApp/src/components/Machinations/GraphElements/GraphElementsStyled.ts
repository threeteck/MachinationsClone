import styled from "@emotion/styled";

type ElementWrapperProps = {
  selected: boolean;
}

const ElementWrapper = styled.div<ElementWrapperProps>`
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  width: 48px;
  height: 48px;
  & svg {
    min-width: 48px!important;
    min-height: 48px!important;
    filter: ${props => props.selected ? "drop-shadow(0px 0px 5px rgb(0 0 0 / 0.4))" : "none"};
  },
`

export const GraphElementsStyled = {
  ElementWrapper
}
