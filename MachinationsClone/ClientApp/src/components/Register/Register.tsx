import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, FormControl, TextField } from '@mui/material';
import { Styled } from './Style';
import { useApi } from '../../common';
import {Auth} from "../../localStorage";

export const Register = () => {
  const navigate = useNavigate();
  const { post } = useApi('/account');

  const [userName, setUserName] = React.useState('');

  const handleUserNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUserName(event.target.value);
  };

  const onRegister = () => {
    post('/register', null, { userName })
      .then(res => res.json())
      .then(data => {
        Auth.setUser(data);
        Auth.setToken(data.token);
        navigate('/');
      })
      .catch((e) => {
        alert(`Register fail ${e}`);
      });
  };

  return (
    <Styled.Root>
      <Styled.Title>Register</Styled.Title>
      <FormControl fullWidth margin="normal">
        <TextField required id="userName" label="User Name" value={userName} onChange={handleUserNameChange} />
      </FormControl>
      <Button variant="contained" color="primary" onClick={onRegister}>
        Register
      </Button>
    </Styled.Root>
  );
};
