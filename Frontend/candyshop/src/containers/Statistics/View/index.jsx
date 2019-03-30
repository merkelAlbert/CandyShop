import React, { useState } from 'react';

import { getAll, USERS, PASTRIES, ORDERS } from '../../../utils/api';

import './style.scss';

const ENTITIES = [
  {
    value: 'User',
    title: 'Пользователи',
    fields: [
      {
        value: 'Name',
        title: 'Имя'
      }
    ]
  },
  {
    value: 'Pastry',
    title: 'Изделия',
    fields: [
      {
        value: 'Request',
        title: 'Спрос'
      },
      {
        value: 'Name',
        title: 'Название'
      },
      {
        value: 'Price',
        title: 'Цена'
      }
    ]
  },
  {
    value: 'Order',
    title: 'Заказы',
    fields: [
      {
        value: 'Sum',
        title: 'Сумма'
      }
    ]
  }
];

const SORTING_TYPES = [
  {
    value: 'Asc',
    title: 'По возрастанию'
  },
  {
    value: 'Desc',
    title: 'По убыванию'
  }
];

const StatisticsView = () => {
  const [entity, setEntity] = useState();
  const [propertyName, setPropertyName] = useState();
  const [sortingType, setSortingType] = useState();
  const [startDate, setStartDate] = useState();
  const [endDate, setEndDate] = useState();
  const [count, setCount] = useState();
  const [result, setResult] = useState([]);
  const [users, setUsers] = useState([]);
  const [user, setUser] = useState([]);
  const handleChange = async e => {
    setResult([]);
    const {
      target: { name, value }
    } = e;
    console.log(name, value);
    switch (name) {
      case 'entity':
        if (value === 'User') {
          setUsers(await getAll(USERS));
        }
        setEntity(value);
        break;
      case 'propertyName':
        setPropertyName(value);
        break;
      case 'sortingType':
        setSortingType(value);
        break;
      case 'startDate':
        setStartDate(value);
        break;
      case 'endDate':
        setEndDate(value);
        break;
      case 'count':
        setCount(value);
        break;
      case 'user':
        setUser(value);
        break;
      default: {
      }
    }
  };

  const handleSubmit = async e => {
    e.preventDefault();
    setResult([]);
    const payload = {
      propertyName,
      sortingType,
      startDate,
      endDate,
      count
    };
    try {
      switch (entity) {
        case 'User': {
          setResult(await getAll(`${USERS}/${user}/orders`, payload));
          break;
        }
        case 'Pastry': {
          setResult(await getAll(PASTRIES, payload));
          break;
        }
        case 'Order': {
          setResult(await getAll(ORDERS, payload));
          break;
        }
        default: {
        }
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="statistics-view">
      <form className="statistics-view__form" onSubmit={handleSubmit}>
        <div className="statistics-view__form-row">
          <select
            className="text statistics-view__form-field"
            value={entity}
            name="entity"
            onChange={handleChange}
          >
            <option selected disabled>
              Сущность
            </option>
            {ENTITIES.map(entity => (
              <option key={entity.value} value={entity.value}>
                {entity.title}
              </option>
            ))}
          </select>
          {entity && (
            <>
              {entity === 'User' && users.length > 0 && (
                <select
                  className="text statistics-view__form-field"
                  value={user}
                  name="user"
                  onChange={handleChange}
                >
                  <option selected>Пользователь</option>
                  {users.map(user => (
                    <option value={user.id}>
                      {user.name} {user.phone}
                    </option>
                  ))}
                </select>
              )}
              {entity !== 'User' && (
                <>
                  <select
                    className="text statistics-view__form-field"
                    value={propertyName}
                    name="propertyName"
                    onChange={handleChange}
                  >
                    <option selected disabled>
                      Поле
                    </option>
                    {ENTITIES.find(el => el.value === entity).fields.map(
                      field => (
                        <option value={field.value}>{field.title}</option>
                      )
                    )}
                  </select>
                  <select
                    className="text statistics-view__form-field"
                    value={sortingType}
                    name="sortingType"
                    onChange={handleChange}
                  >
                    <option selected disabled>
                      Тип сортировки
                    </option>
                    {SORTING_TYPES.map(field => (
                      <option value={field.value}>{field.title}</option>
                    ))}
                  </select>
                </>
              )}
            </>
          )}
        </div>
        <div className="statistics-view__form-row">
          <input
            className="text statistics-view__form-field"
            type="date"
            name="startDate"
            placeholder="Начальная дата"
            value={startDate}
            onChange={handleChange}
          />
          <input
            className="text statistics-view__form-field"
            type="date"
            name="endDate"
            placeholder="Конечная дата"
            value={endDate}
            onChange={handleChange}
          />
        </div>
        <div className="statistics-view__form-row">
          <input
            className="text statistics-view__form-field"
            type="number"
            name="count"
            placeholder="Число элементов"
            value={count}
            onChange={handleChange}
          />
        </div>
        <input
          className="button statistics-view__form-submit-button"
          type="submit"
          value="Применить"
        />
      </form>
      {(result.length > 0 || (result.orders && result.orders.length > 0)) && (
        <div>
          {entity === 'Order' || entity === 'User' ? (
            <>
              <div>Общая сумма: {result.sum}</div>
              {result.orders.map(order => (
                <div
                  style={{
                    border: '1px solid black',
                    margin: '5px 0',
                    padding: '5px'
                  }}
                >
                  <div>{order.user.name}</div>
                  <div>
                    {order.pastries.map(({ amount, pastry }) => (
                      <div key={pastry.id}>
                        <p>
                          <strong>{pastry.name}</strong> | {pastry.price} ₽ |{' '}
                          {amount} шт.
                        </p>
                      </div>
                    ))}
                  </div>
                  <div>Сумма заказа: {order.sum} ₽</div>
                  <div>
                    {new Date(order.creationDate).toLocaleDateString('ru')}
                  </div>
                </div>
              ))}
            </>
          ) : (
            entity === 'Pastry' && (
              <>
                {result.map(pastry => (
                  <div
                    style={{
                      border: '1px solid black',
                      margin: '5px 0',
                      padding: '5px'
                    }}
                  >
                    <div>{pastry.name}</div>
                    <div>{pastry.price} ₽</div>
                    <div>Всего заказано за период: {pastry.request}</div>
                  </div>
                ))}
              </>
            )
          )}
        </div>
      )}
    </div>
  );
};

export default StatisticsView;
