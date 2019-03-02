import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { USERS, getAll, del } from '../../../utils/api';

import './style.scss';
import User from '../../../components/User';

const UsersView = () => {
  const [users, setUsers] = useState([]);
  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const data = await getAll(USERS);
      setUsers(data);
    } catch (error) {
      console.log(error);
    }
  };

  const handleUserDelete = async userId => {
    try {
      await del(USERS, userId);
      setUsers(users.filter(user => user.id !== userId));
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="users-view">
      <Link className="link" to="/users/add">
        <button className="button users-view__add-button">
          добавить пользователя
        </button>
      </Link>
      <div className="users-view__users">
        {users.map(user => (
          <User
            key={user.id}
            user={user}
            onDelete={handleUserDelete}
            className="users-view__user"
          />
        ))}
      </div>
    </div>
  );
};

export default UsersView;
