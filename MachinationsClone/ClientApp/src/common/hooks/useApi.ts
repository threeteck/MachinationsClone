import { useCallback } from 'react';
import { useClient } from '../../client';

export const useApi = (baseMethod?: string, baseUrl?: string) => {
  const client = useClient();
  const serverUrl = baseUrl || client.host;

  const get = useCallback(
    (method?: string | null, params?: Record<string, string> | null) => {
      return fetch(`${serverUrl}${baseMethod || ''}${method || ''}${params ? `?${new URLSearchParams(params)}` : ''}`, {
        method: 'GET',
        credentials: 'same-origin',
        headers: client.getHeaders(),
      });
    },
    [baseMethod, serverUrl, client, client.headers],
  );

  const post = useCallback(
    (method?: string | null, params?: Record<string, string> | null, body?: any) => {
      return fetch(`${serverUrl}${baseMethod || ''}${method || ''}${params ? `?${new URLSearchParams(params)}` : ''}`, {
        method: 'POST',
        credentials: 'same-origin',
        headers: client.getHeaders(),
        ...(body ? { body: JSON.stringify(body) } : {}),
      });
    },
    [baseMethod, serverUrl, client, client.headers],
  );

  return { get, post };
};
