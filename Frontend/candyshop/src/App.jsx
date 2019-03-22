import React, { Component } from 'react';
import { Route, Switch, BrowserRouter } from 'react-router-dom';

import Header from './components/Header';
import Users from './containers/Users';
import Pastries from './containers/Pastries';
import Orders from './containers/Orders';
import Statistics from './containers/Statistics';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <Header />
          <Switch>
            <Route path="/users" component={Users} />
            <Route path="/pastries" component={Pastries} />
            <Route path="/orders" component={Orders} />
            <Route path="/statistics" component={Statistics} />
          </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
