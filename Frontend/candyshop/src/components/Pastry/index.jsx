import React from 'react';

import { Link } from 'react-router-dom';

import './style.scss';
import EditIcon from '../Icons/Edit';
import DeleteIcon from '../Icons/Delete';

const types = [
  { value: 0, title: 'Торт' },
  { value: 1, title: 'Кекс' },
  { value: 2, title: 'Печенье' }
];

const Pastry = ({ pastry, className, onDelete, withoutActions, ...props }) => (
  <div className={className + ' pastry'} {...props}>
    <img
      className="pastry__image"
      src={`https://avatars.dicebear.com/v2/jdenticon/${pastry.id}.svg`}
      alt="тортик"
    />
    <div className="pastry__info">
      <h3>{pastry.name}</h3>
      <p>{types.filter(type => type.value === pastry.pastryType)[0].title}</p>
      <p>{pastry.price} ₽</p>
      <p>{pastry.description}</p>
      <div>{pastry.compound}</div>
    </div>
    {!withoutActions && (
      <div className="pastry__actions">
        <Link to={`/pastries/${pastry.id}/edit`}>
          <EditIcon className="pastry__icon" />
        </Link>
        <DeleteIcon
          className="pastry__icon pastry__delete-icon"
          onClick={() => onDelete(pastry.id)}
        />
      </div>
    )}
  </div>
);

export default Pastry;
