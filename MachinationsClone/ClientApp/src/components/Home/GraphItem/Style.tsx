import { styled } from '@mui/material';

const Root = styled('div')({
    width: '100%',
    display: 'grid',
    gridTemplateColumns: 'repeat(3, 1fr)',
    gridGap: 16,
    boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.5)',
    borderRadius: 8,
    padding: 16,
    flexShrink: 0,
    '&:hover': {
        boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.8)',
        backgroundColor: 'rgba(0, 0, 0, 0.05)',
    },
    '&:active': {
        boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.8)',
        backgroundColor: 'rgba(0, 0, 0, 0.1)',
    },
    '&:focus': {
        boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.8)',
        backgroundColor: 'rgba(0, 0, 0, 0.1)',
    },
    'cursor': 'pointer',
});

const Text = styled('div')({
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
    height: '100%',
});

export const Styled = {
    Root,
    Text
};
