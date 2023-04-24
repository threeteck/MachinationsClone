import { styled } from '@mui/material';

const Root = styled('div')({
    display: 'flex',
    flexDirection: 'row',
    height: 48,
    width: 48,
});

const AmountLabel = styled('div')({
    position: 'absolute',
    top: '27%',
    left: 0,
    width: '100%',
    height: '100%',
    display: 'flex',
    justifyContent: 'center',
});

const NameLabel = styled('div')({
    position: 'absolute',
    top: '-60%',
    left: '-45%',
    width: '200%',
    height: '100%',
    display: 'flex',
    justifyContent: 'center',
});

const PushLabel = styled('div')({
    position: 'absolute',
    top: '75%',
    left: '35%',
    width: '100%',
    height: '100%',
    display: 'flex',
    justifyContent: 'center',
    fontWeight: 'bold',
});

const AutoLabel = styled('div')({
    position: 'absolute',
    top: '-20%',
    left: '40%',
    width: '100%',
    height: '100%',
    display: 'flex',
    justifyContent: 'center',
    fontWeight: 'bold',
    fontSize: '1.4rem',
});

const OnStartLabel = styled('div')({
    position: 'absolute',
    top: '-20%',
    left: '40%',
    width: '100%',
    height: '100%',
    display: 'flex',
    justifyContent: 'center',
    fontWeight: 'bold',
});

export const Styled = {
    Root,
    AmountLabel,
    NameLabel,
    PushLabel,
    AutoLabel,
    OnStartLabel
};
