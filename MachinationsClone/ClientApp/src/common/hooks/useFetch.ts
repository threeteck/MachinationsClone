import {useCallback, useState} from 'react';
import {useClient} from "../../client";

export const useFetch = (baseMethod?: string, baseUrl?: string) => {
    const client = useClient();
    const serverUrl = baseUrl || client.host;
    
    const [data, setData] = useState<any>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<any>(null);
    
    const get = useCallback(async (method?: string, params?: Record<string, string> | null) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`${serverUrl}${baseMethod || ''}${method || ''}${params ? `?${  new URLSearchParams(params)}` : ''}`,
                {
                    method: 'GET',
                    credentials: 'same-origin',
                    headers: client.getHeaders()
                });
            if (response.ok) {
                const json = await response.json();
                setData(json);
            } else {
                const json = await response.json();
                setError(json);
            }
        } catch (e) {
            setError(e);
        } finally {
            setLoading(false);
        }
    }, [baseMethod, serverUrl, setData, setLoading, setError, client, client.headers]);
    
    const post = useCallback(async (method?: string, params?: Record<string, string> | null, body?: any) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`${serverUrl}${baseMethod || ''}${method || ''}${params ? `?${  new URLSearchParams(params)}` : ''}`,
                {
                    method: 'POST',
                    credentials: 'same-origin',
                    headers: client.getHeaders(),
                    ...(body ? {body: JSON.stringify(body)} : {})
                });
            if (response.ok) {
                const json = await response.json();
                setData(json);
            } else {
                const json = await response.json();
                setError(json);
            }
        } catch (e) {
            setError(e);
        } finally {
            setLoading(false);
        }
    }, [baseMethod, serverUrl, setData, setLoading, setError, client, client.headers]);
    
    return {data, loading, error, get, post};
}
