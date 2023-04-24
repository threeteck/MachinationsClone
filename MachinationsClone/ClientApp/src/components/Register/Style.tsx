import { styled } from '@mui/material';

const Root = styled('div')({
    width: '40%',
    height: 468,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.5)',
    borderRadius: 8,
    padding: 32,
});

const Title = styled('div')({
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 16,
});

export const Styled = {
    Root,
    Title
};
