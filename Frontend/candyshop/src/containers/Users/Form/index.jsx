import React, { useState, useEffect } from 'react';
import { withRouter } from 'react-router-dom';

import { USERS, post, getOne, put } from '../../../utils/api';

import './style.scss';

const UsersForm = ({
  history,
  match: {
    params: { userId }
  },
  ...props
}) => {
  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');

  useEffect(() => {
    if (userId) {
      fetchUser();
    }
  }, []);

  const fetchUser = async () => {
    try {
      const data = await getOne(USERS, userId);
      setName(data.name);
      setPhone(data.phone);
    } catch (error) {
      console.log(error);
    }
  };

  const handleChange = e => {
    switch (e.target.name) {
      case 'name':
        setName(e.target.value);
        break;
      case 'phone':
        setPhone(e.target.value);
        break;
      default: {
      }
    }
  };

  const handleSubmit = async e => {
    e.preventDefault();
    const payload = {
      name,
      phone
    };
    try {
      if (!userId) await post(USERS, payload);
      else await put(USERS, userId, payload);
      history.push('/users');
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <form className="users-form" onSubmit={handleSubmit}>
      <input
        className="text users-form__field"
        type="text"
        name="name"
        placeholder="Имя"
        value={name}
        onChange={handleChange}
      />
      <input
        className="text users-form__field"
        type="text"
        name="phone"
        placeholder="Телефон"
        value={phone}
        onChange={handleChange}
      />
      <input
        className="button users-form__submit-button"
        type="submit"
        value={!userId ? 'Добавить' : 'Изменить'}
      />
    </form>
  );
};

export default withRouter(UsersForm);
