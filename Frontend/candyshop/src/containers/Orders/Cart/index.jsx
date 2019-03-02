import React, { useState, useEffect } from 'react';
import { withRouter } from 'react-router-dom';

import {
  getAll,
  post,
  put,
  USERS,
  PASTRIES,
  ORDERS,
  getOne
} from '../../../utils/api';

import './style.scss';
import User from '../../../components/User';
import Pastry from '../../../components/Pastry';

const OrdersCart = ({
  history,
  match: {
    params: { orderId }
  }
}) => {
  const [users, setUsers] = useState([]);
  const [pastries, setPastries] = useState([]);
  const [amounts, setAmounts] = useState([]);
  const [selectedUser, setSelectedUser] = useState({});
  const [selectedPastries, setSelectedPastries] = useState([]);
  useEffect(() => {
    fetchData();
  }, []);

  const fetchOrder = async () => {
    const order = await getOne(ORDERS, orderId);
    setSelectedUser(order.user.id);
    const pastriesIds = [];
    for (const pastry of order.pastries) {
      if (!pastriesIds.includes(pastry.id)) {
        pastriesIds.push(pastry.id);
      }
    }
    for (const pastryId of pastriesIds) {
      const count = order.pastries.filter(p => p.id === pastryId).length;
      handleAmountChange(count, pastryId);
    }
    setSelectedPastries(pastriesIds);
  };

  const fetchData = async () => {
    try {
      if (orderId) {
        fetchOrder();
      }
      const users = await getAll(USERS);
      const pastries = await getAll(PASTRIES);
      setUsers(users);
      setPastries(pastries);
    } catch (error) {
      console.log(error);
    }
  };

  const handleAmountChange = (value, pastryId) => {
    const amount = amounts.find(amount => amount.pastryId === pastryId);
    if (amount != null) {
      if (value === 0) {
        setAmounts(prevAmounts =>
          prevAmounts.filter(amount => amount.pastryId !== pastryId)
        );
      } else {
        setAmounts(prevAmounts =>
          prevAmounts.map(amount => {
            if (amount.pastryId === pastryId) {
              amount.value = value;
            }
            return amount;
          })
        );
      }
    } else {
      setAmounts(prevAmounts => [...prevAmounts, { pastryId, value }]);
    }
  };

  const handleAmountInputChange = (e, pastryId) => {
    const { value } = e.target;
    handleAmountChange(value, pastryId);
  };

  const handleUserClick = userId => {
    const user = users.find(user => user.id === userId);
    if (user && selectedUser !== user.id) setSelectedUser(user.id);
    else setSelectedUser({});
  };

  const handlePastryClick = pastryId => {
    const pastry = pastries.find(pastry => pastry.id === pastryId);
    if (pastry && !selectedPastries.includes(pastry.id)) {
      setSelectedPastries(prevPastries => [...prevPastries, pastry.id]);
      handleAmountChange(1, pastryId);
    } else {
      setSelectedPastries(prevPastries =>
        prevPastries.filter(id => id !== pastryId)
      );
      handleAmountChange(0, pastryId);
    }
  };

  const handleSubmitButtonClick = async () => {
    const pastriesIds = [];
    for (const amount of amounts) {
      if (amount.value && amount.value > 0) {
        const pastryId = selectedPastries.find(
          pastry => pastry === amount.pastryId
        );
        for (let i = 0; i < amount.value; i++) {
          pastriesIds.push(pastryId);
        }
      }
    }
    try {
      if (!orderId) await post(ORDERS, { userId: selectedUser, pastriesIds });
      else await put(ORDERS, orderId, { userId: selectedUser, pastriesIds });
      history.push('/orders');
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="orders-cart">
      <div className="orders-cart__containers">
        <div className="orders-cart__container">
          {users.map(user => (
            <User
              key={user.id}
              user={user}
              className={
                user.id !== selectedUser
                  ? 'orders-cart__item'
                  : 'orders-cart__item orders-cart__item--selected'
              }
              withoutActions
              onClick={() => handleUserClick(user.id)}
            />
          ))}
        </div>
        <div className="orders-cart__container">
          {pastries.map(pastry => (
            <div key={pastry.id}>
              <Pastry
                pastry={pastry}
                className={
                  !selectedPastries.includes(pastry.id)
                    ? 'orders-cart__item'
                    : 'orders-cart__item orders-cart__item--selected'
                }
                withoutActions
                onClick={() => handlePastryClick(pastry.id)}
              />
              <input
                type="number"
                placeholder="Количество"
                defaultValue={
                  amounts.find(amount => amount.pastryId === pastry.id) &&
                  amounts.find(amount => amount.pastryId === pastry.id).value
                }
                className={
                  selectedPastries.includes(pastry.id)
                    ? 'text orders-cart__item'
                    : 'text orders-cart__item orders-cart__item--hidden'
                }
                onChange={e => handleAmountInputChange(e, pastry.id)}
              />
            </div>
          ))}
        </div>
      </div>
      <input
        type="button"
        value={orderId ? 'Изменить' : 'Добавить'}
        className={
          Object.keys(selectedUser).length === 0 ||
          selectedPastries.length === 0
            ? 'button button--disabled orders-cart__button'
            : 'button orders-cart__button'
        }
        disabled={
          Object.keys(selectedUser).length === 0 ||
          selectedPastries.length === 0
        }
        onClick={handleSubmitButtonClick}
      />
    </div>
  );
};

export default withRouter(OrdersCart);
