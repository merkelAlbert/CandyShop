import React from 'react';
import { Route, Switch } from 'react-router-dom';

import OrdersView from './View';
import OrdersCart from './Cart';

const Orders = () => (
  <Switch>
    <Route exact path="/orders" component={OrdersView} />
    <Route path="/orders/add" component={OrdersCart} />
    <Route path="/orders/:orderId/edit" component={OrdersCart} />
  </Switch>
);

export default Orders;
