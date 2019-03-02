import React from 'react';
import { Route, Switch } from 'react-router-dom';

import Form from './Form';
import UsersView from './View';

const Users = () => (
  <Switch>
    <Route exact path="/users" component={UsersView} />
    <Route path="/users/add" component={Form} />
    <Route path="/users/:userId/edit" component={Form} />
  </Switch>
);

export default Users;
