import { useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';
import { Styled } from './Style';
import { Auth } from '../../localStorage';
import { useApi } from '../../common';

export const Header = () => {
  const { post } = useApi('/account');
  const navigate = useNavigate();
  const isLogged = Auth.isLoggedIn();
  const user = Auth.getUser();
  const userName = user ? user.username : '';
  console.log(user);

  const logout = () => {
    post('/logout').then(() => {
      Auth.removeUser();
      navigate('/login');
    });
  };
  // color of buttons is white
  const loggedIn = (
    <div>
      {userName}
      <Button variant="outlined" color="secondary" style={{ marginLeft: 16 }} onClick={logout}>
        Logout
      </Button>
    </div>
  );

  const loggedOut = (
    <div>
      <Button variant="outlined" color="secondary" onClick={() => navigate('/login')}>
        Login
      </Button>
      <Button variant="outlined" color="secondary" style={{ marginLeft: 8 }} onClick={() => navigate('/register')}>
        Register
      </Button>
    </div>
  );

  return (
    <Styled.Root>
      <Styled.Title onClick={() => navigate('/')}>Home</Styled.Title>
      {isLogged ? loggedIn : loggedOut}
    </Styled.Root>
  );
};
