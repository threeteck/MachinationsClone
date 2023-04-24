import { styled } from '@mui/material';

const Root = styled('div')({
    backgroundColor: 'white',
    borderRadius: '10px',
    padding: '12px',
    height: '70%',
    display: 'grid',
    gridTemplateRows: '1fr 1fr',
    gap: '0 24px',
});

const ElementsBarWrapper = styled('div')({
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'start',
    alignItems: 'center',
});

const Wrapper = styled('div')({
    display: 'grid',
    gridTemplateRows: '1fr 1fr',
    gap: '0 0px',
    padding: '12px',
    marginTop: '28px',
});

const ElementWrapper = styled('div')({
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    padding: '12px',
    borderRadius: '10px',
    cursor: 'pointer',
    width: '32px',
    height: '32px',
    '&:hover': {
        backgroundColor: '#e0e0e0'
    }
});

export const Styled = {
    Root,
    Wrapper,
    ElementsBarWrapper,
    ElementWrapper
};
