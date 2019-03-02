import { useState, useEffect, useCallback } from 'react';

import { USERS, getAll, del } from '../utils/api';

export default () => {
  const [users, setUsers] = useState([]);
  useEffect(() => {
    fetchUsers();
  }, []);

  const deleteUser = useCallback(async userId => {
    try {
      console.log(users);
      await del(USERS, userId);
      setUsers(users.filter(user => user.id !== userId));
    } catch (error) {
      console.log(error);
    }
  }, []);

  const fetchUsers = async () => {
    try {
      const data = await getAll(USERS);
      setUsers(data);
    } catch (error) {
      console.log(error);
    }
  };

  return {
    users,
    deleteUser
  };
};
