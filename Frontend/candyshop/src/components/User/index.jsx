import React from 'react';

import { Link } from 'react-router-dom';

import './style.scss';
import EditIcon from '../Icons/Edit';
import DeleteIcon from '../Icons/Delete';

const User = ({ user, className, onDelete, withoutActions, ...props }) => (
  <div className={className + ' user'} {...props}>
    <img
      className="user__image"
      src={`https://avatars.dicebear.com/v2/avataaars/${user.id}.svg`}
      alt="челик"
    />
    <div className="user__info">
      <div>{user.name}</div>
      <div>{user.phone}</div>
    </div>
    {!withoutActions && (
      <div className="user__actions">
        <Link to={`/users/${user.id}/edit`}>
          <EditIcon className="user__icon" />
        </Link>
        <DeleteIcon
          className="user__icon user__delete-icon"
          onClick={() => onDelete(user.id)}
        />
      </div>
    )}
  </div>
);

export default User;
