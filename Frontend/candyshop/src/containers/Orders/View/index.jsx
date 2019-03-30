import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { ORDERS, getAll, del } from '../../../utils/api';

import './style.scss';
import EditIcon from '../../../components/Icons/Edit';
import DeleteIcon from '../../../components/Icons/Delete';

const OrdersView = () => {
  const [orders, setOrders] = useState([]);
  useEffect(() => {
    fetchOrders();
  }, []);

  const handleDeleteIconClick = async orderId => {
    try {
      await del(ORDERS, orderId);
      setOrders(orders.filter(order => order.id !== orderId));
    } catch (error) {
      console.log(error);
    }
  };

  const fetchOrders = async () => {
    try {
      const { orders } = await getAll(ORDERS);
      setOrders(orders);
    } catch (error) {
      console.log(error);
    }
  };

  console.log(orders);

  return (
    <div className="orders-view">
      <Link className="link" to="/orders/add">
        <button className="button orders-view__add-button">
          добавить заказ
        </button>
      </Link>
      <div className="orders-view__orders">
        {orders.map(order => {
          return (
            <div key={order.id} className="orders-view__order">
              <img
                className="orders-view__order-image"
                src={`https://avatars.dicebear.com/v2/identicon/${
                  order.id
                }.svg`}
                alt="заказик"
              />
              <div className="orders-view__order-info">
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
                  <strong>Сумма заказа:</strong> {order.sum} ₽
                </div>
                <div>
                  {new Date(order.creationDate).toLocaleDateString('ru')}
                </div>
              </div>
              <div className="orders-view__actions">
                <Link to={`/orders/${order.id}/edit`}>
                  <EditIcon className="orders-view__icon" />
                </Link>
                <DeleteIcon
                  className="orders-view__icon orders-view__delete-icon"
                  onClick={() => handleDeleteIconClick(order.id)}
                />
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default OrdersView;
