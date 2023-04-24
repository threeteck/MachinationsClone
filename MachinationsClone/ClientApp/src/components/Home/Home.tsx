import { useEffect } from 'react';
import { Button, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Styled } from './Style';
import { GraphItem } from './GraphItem/GraphItem';
import { useFetch } from '../../common';
import {Auth} from "../../localStorage";

export const Home = () => {
  const navigate = useNavigate();
  const { data, get } = useFetch('/graph');
  useEffect(() => {
    get('/getAll');
  }, []);
  
  if (!Auth.isLoggedIn())
    navigate('/login');

  console.log(data);

  return (
    <Styled.Root>
      <Styled.Header>
        <Typography variant="h5" component="h2">
          Your graphs
        </Typography>
        <Button variant="contained" color="primary" onClick={() => navigate('/graphs/create')}>
          Create new graph
        </Button>
      </Styled.Header>
      <Styled.Wrapper>
        {data?.map((graph: any) => (
          <GraphItem key={graph.id} id={graph.id} name={graph.name} createdAt={graph.createdAt} />
        ))}
      </Styled.Wrapper>
    </Styled.Root>
  );
};
