import { useEffect, useState } from 'react';
import moment from 'moment';

export const useInterval = (interval = 1000) => {
    const [currentTime, setCurrentTime] = useState(moment());

    useEffect(() => {
        const target = setInterval(() => {
            const time = moment();
            setCurrentTime(time);
        }, interval);

        return () => {
            clearInterval(target);
        };
    }, [interval]);

    return currentTime;
};
