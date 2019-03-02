import React from 'react';
import { Route, Switch } from 'react-router-dom';

import Form from './Form';
import PastriesView from './View';

const Pastries = () => (
  <Switch>
    <Route exact path="/pastries" component={PastriesView} />
    <Route path="/pastries/add" component={Form} />
    <Route path="/pastries/:pastryId/edit" component={Form} />
  </Switch>
);

export default Pastries;
