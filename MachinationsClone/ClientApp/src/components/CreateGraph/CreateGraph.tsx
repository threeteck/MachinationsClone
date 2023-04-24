import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Styled } from './Style';
import { useApi } from '../../common';
import {Button, FormControl, TextField} from "@mui/material";

export const CreateGraph = () => {
  const navigate = useNavigate();
  const { post } = useApi('/graph');

  const [name, setName] = React.useState('');
  const [description, setDescription] = React.useState('');

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };
  
  const handleDescriptionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setDescription(event.target.value);
  };

  const onCreate = () => {
    post('/create', null, { name, description }).then(() => {
      navigate('/graphs');
    });
  };

  return (
    <Styled.Root>
      <Styled.Title>Create Graph</Styled.Title>
      <FormControl fullWidth margin="normal">
        <TextField required id="name" label="Name" value={name} onChange={handleNameChange} />
      </FormControl>
      <FormControl fullWidth margin="normal">
        <TextField
          id="description"
          label="Description"
          value={description}
          onChange={handleDescriptionChange}
          multiline
          minRows={4}
        />
      </FormControl>
      <Button variant="contained" color="primary" onClick={onCreate}>
        Create
      </Button>
    </Styled.Root>
  );
};
