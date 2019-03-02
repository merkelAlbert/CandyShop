import React from 'react';
import { Link } from 'react-router-dom';

import './style.scss';

const Header = () => (
  <header className="header">
    <Link className="link" to="/pastries" >Изделия</Link>
    <Link className="link" to="/users" >Пользователи</Link>
    <Link className="link" to="/orders" >Заказы</Link>
  </header>
);

export default Header;
