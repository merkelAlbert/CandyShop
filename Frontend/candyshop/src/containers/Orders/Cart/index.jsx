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
  const [state, setState] = useState({
    users: [],
    pastries: [],
    amounts: [],
    selectedUser: {},
    selectedPastries: []
  });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchOrder = async () => {
    const order = await getOne(ORDERS, orderId);
    setState(prevState => ({ ...prevState, selectedUser: order.user.id }));
    //setSelectedUser(order.user.id);
    const pastriesIds = order.pastries.map(pastry => pastry.id);


    for (const pastryId of pastriesIds) {
      const {
        pastry: { id },
        amount
      } = order.pastries.find(p => p.id === pastryId);
      handleAmountChange(amount, id);
    }
    //setSelectedPastries(pastriesIds);
    setState(prevState => ({ ...prevState, selectedPastries: pastriesIds }));
  };

  const fetchData = async () => {
    try {
      if (orderId) {
        fetchOrder();
      }
      const users = await getAll(USERS);
      const pastries = await getAll(PASTRIES);
      //setUsers(users);
      setState(prevState => ({ ...prevState, users, pastries }));
      //setPastries(pastries);
    } catch (error) {
      console.log(error);
    }
  };

  const handleAmountChange = (value, pastryId) => {
    const amount = state.amounts.find(amount => amount.pastryId === pastryId);
    if (amount != null) {
      if (value === 0) {
        setState(prevState => ({
          ...prevState,
          amounts: prevState.amounts.filter(
            amount => amount.pastryId !== pastryId
          )
        }));
      } else {
        setState(prevState => ({
          ...prevState,
          amounts: prevState.amounts.map(amount => {
            if (amount.pastryId === pastryId) {
              amount.value = value;
            }
            return amount;
          })
        }));
      }
    } else {
      setState(prevState => ({
        ...prevState,
        amounts: [...prevState.amounts, { pastryId, value }]
      }));
    }
  };

  const handleAmountInputChange = (e, pastryId) => {
    const { value } = e.target;
    handleAmountChange(value, pastryId);
  };

  const handleUserClick = userId => {
    if (!orderId) {
      const user = state.users.find(user => user.id === userId);
      if (user && state.selectedUser !== user.id)
        setState(prevState => ({ ...prevState, selectedUser: user.id }));
      else setState(prevState => ({ ...prevState, selectedUser: {} }));
    }
  };

  const handlePastryClick = pastryId => {
    const pastry = state.pastries.find(pastry => pastry.id === pastryId);
    if (pastry && !state.selectedPastries.includes(pastry.id)) {
      setState(prevState => ({
        ...prevState,
        selectedPastries: [...prevState.selectedPastries, pastry.id]
      }));
      handleAmountChange(1, pastryId);
    } else {
      setState(prevState => ({
        ...prevState,
        selectedPastries: prevState.selectedPastries.filter(
          id => id !== pastryId
        )
      }));
      handleAmountChange(0, pastryId);
    }
  };

  const handleSubmitButtonClick = async () => {
    const pastriesInfos = [];
    for (const amount of state.amounts) {
      if (amount.value && amount.value > 0) {
        const pastryId = state.selectedPastries.find(
          pastry => pastry === amount.pastryId
        );
        pastriesInfos.push({ pastryId, amount: amount.value });
      }
    }
    try {
      if (!orderId)
        await post(ORDERS, { userId: state.selectedUser, pastriesInfos });
      else
        await put(ORDERS, orderId, {
          userId: state.selectedUser,
          pastriesInfos
        });
      history.push('/orders');
    } catch (error) {
      console.log(error);
    }
  };

  console.log(state)

  return (
    <div className="orders-cart">
      <div className="orders-cart__containers">
        <div className="orders-cart__container">
          {state.users.map(user => (
            <User
              key={user.id}
              user={user}
              className={
                user.id !== state.selectedUser
                  ? 'orders-cart__item'
                  : 'orders-cart__item orders-cart__item--selected'
              }
              withoutActions
              onClick={() => handleUserClick(user.id)}
            />
          ))}
        </div>
        <div className="orders-cart__container">
          {state.pastries.map(pastry => (
            <div key={pastry.id}>
              <Pastry
                pastry={pastry}
                className={
                  !state.amounts.map(amount=>amount.pastryId).includes(pastry.id)
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
                  state.amounts.find(amount => amount.pastryId === pastry.id) &&
                  state.amounts.find(amount => amount.pastryId === pastry.id)
                    .value
                }
                className={
                  state.selectedPastries.includes(pastry.id)
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
          Object.keys(state.selectedUser).length === 0 ||
          state.selectedPastries.length === 0
            ? 'button button--disabled orders-cart__button'
            : 'button orders-cart__button'
        }
        disabled={
          Object.keys(state.selectedUser).length === 0 ||
          state.selectedPastries.length === 0
        }
        onClick={handleSubmitButtonClick}
      />
    </div>
  );
};

export default withRouter(OrdersCart);
