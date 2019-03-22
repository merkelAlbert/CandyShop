import React from 'react';
import { Route, Switch } from 'react-router-dom';

import StatisticsView from './View';

const Pastries = () => (
  <Switch>
    <Route exact path="/statistics" component={StatisticsView} />
  </Switch>
);

export default Pastries;
