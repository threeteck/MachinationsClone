import { styled } from '@mui/material';

const Root = styled('div')({
    display: 'grid',
    gridTemplateColumns: '1.5fr 1fr',
    gridTemplateRows: '1fr',
    gap: '0 24px',
    padding: '12px',
});

const ControlPanelWrapper = styled('div')({
    display: 'grid',
    gridTemplateRows: '1.5fr 1fr',
    gap: '0 24px',
    padding: '12px',
});

const MainArea = styled('div')({
    width: 1024,
    height: 768,
    backgroundColor: 'white',
    borderRadius: '10px',
});

const FlowWrapper = styled('div')({
    width: '100%',
    height: '100%',
    flexGrow: 1
});

export const Styled = {
    Root,
    MainArea,
    ControlPanelWrapper,
    FlowWrapper
};
