import { useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';
import { Styled } from './Style';
import { useApi } from '../../../common';

export type GraphItemProps = {
  id: string;
  name: string;
  createdAt: string;
};

export const GraphItem = ({ id, name, createdAt }: GraphItemProps) => {
  const navigate = useNavigate();
  const { post } = useApi(`/graph/${id}`);
  const date = new Date(createdAt);
  const formattedDate = `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`;

  const onDelete = () => {
    post('/delete').then(() => {
      window.location.reload();
    });
  };

  return (
    <Styled.Root>
      <Styled.Text onClick={() => navigate(`/graph/${id}`)}>{name}</Styled.Text>
      <Styled.Text onClick={() => navigate(`/graph/${id}`)}>{formattedDate}</Styled.Text>
      <Button variant="outlined" onClick={onDelete} style={{borderColor: 'red', color: 'red'}}>
        Delete
      </Button>
    </Styled.Root>
  );
};
