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
        value: 'Price',
        title: 'Сумма'
      },
      {
        value: 'Amount',
        title: 'Число изделий'
      },
      {
        value: 'CreationDate',
        title: 'Дата создания'
      },
      {
        value: 'PastryName',
        title: 'Изделие'
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
  },
  {
    value: 'Equals',
    title: 'Равно'
  }
];

const StatisticsView = () => {
  const [entity, setEntity] = useState();
  const [propertyName, setPropertyName] = useState();
  const [sortingType, setSortingType] = useState();
  const [valueToEqual, setValueToEqual] = useState();
  const [count, setCount] = useState();
  const [result, setResult] = useState([]);
  const handleChange = e => {
    setResult([]);
    switch (e.target.name) {
      case 'entity':
        setEntity(e.target.value);
        break;
      case 'propertyName':
        setPropertyName(e.target.value);
        break;
      case 'sortingType':
        setSortingType(e.target.value);
        break;
      case 'valueToEqual':
        setValueToEqual(e.target.value);
        break;
      case 'count':
        setCount(e.target.value);
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
      valueToEqual,
      count
    };
    try {
      switch (entity) {
        case 'User': {
          setResult(await getAll(USERS, payload));
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

  console.log(result);
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
              <select
                className="text statistics-view__form-field"
                value={propertyName}
                name="propertyName"
                onChange={handleChange}
              >
                <option selected disabled>
                  Поле
                </option>
                {ENTITIES.find(el => el.value === entity).fields.map(field => (
                  <option value={field.value}>{field.title}</option>
                ))}
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
        </div>
        <div className="statistics-view__form-row">
          {sortingType === 'Equals' && (
            <input
              className="text statistics-view__form-field"
              type="text"
              name="valueToEqual"
              placeholder="Значение"
              value={valueToEqual}
              onChange={handleChange}
            />
          )}
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
      {result.length > 0 && (
        <div>
          {entity === 'Order'
            ? result.map(order => (
                <div style={{ margin: '30px' }}>
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
                  <div>
                    {new Date(order.creationDate).toLocaleDateString('ru')}
                  </div>
                </div>
              ))
            : result.map(item => (
                <div style={{ margin: '20px' }}>
                  {Object.keys(item).map(key => (
                    <>{key !== 'id' && <div>{item[key]}</div>}</>
                  ))}
                </div>
              ))}
        </div>
      )}
    </div>
  );
};

export default StatisticsView;
