import { styled } from '@mui/material';

const Root = styled('div')({
    width: '60%',
    height: 768,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.5)',
    borderRadius: 8,
    padding: '32px 64px'
});

const Wrapper = styled('div')({
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'start',
    padding: 32,
    paddingTop: 8,
    marginTop: 18,
    width: '100%',
    height: '100%',
    // backgroundColor: 'black'
});

const Header = styled('div')({
    width: '100%',
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 16
});

export const Styled = {
    Root,
    Wrapper,
    Header
};
