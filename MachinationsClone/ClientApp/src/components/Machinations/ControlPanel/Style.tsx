import { styled } from '@mui/material';

const Root = styled('div')({
    backgroundColor: 'white',
    borderRadius: '10px',
    padding: '12px',
    height: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'start',
});

const ButtonWrapper = styled('div')({
    marginTop: 'auto',
    justifySelf: 'flex-end',
    width: '100%',
    '& > *': {
        width: '100%'
    },
    '& > *:nth-child(n+2)': {
        marginTop: '12px'
    }
})

export const Styled = {
    Root,
    ButtonWrapper
};
