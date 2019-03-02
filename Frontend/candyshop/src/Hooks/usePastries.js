import { useState, useEffect } from 'react';

import { PASTRIES, getAll } from '../utils/api';

export default () => {
  const [pastries, setPastries] = useState([]);
  useEffect(() => {
    fetchPastries();
  }, []);

  const fetchPastries = async () => {
    try {
      const data = await getAll(PASTRIES);
      setPastries(data);
    } catch (error) {
      console.log(error);
    }
  };
  return pastries;
};
