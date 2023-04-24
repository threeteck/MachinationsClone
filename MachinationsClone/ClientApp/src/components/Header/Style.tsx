import { styled } from '@mui/material';

const Root = styled('div')({
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: '0 24px',
    height: '64px',
    backgroundColor: '#17192e',
    color: '#fff'
});

const Title = styled('div')({
    fontSize: '24px',
    fontWeight: 'bold',
    cursor: 'pointer'
});

export const Styled = {
    Root,
    Title
};
