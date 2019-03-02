import React, { Component } from 'react';
import { Route, Switch, BrowserRouter } from 'react-router-dom';

import Header from './components/Header';
import Users from './containers/Users';
import Pastries from './containers/Pastries';
import Orders from './containers/Orders';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <Header />
          <Switch>
            {/* <Route exact path="/cart" component={}/> */}
            <Route path="/users" component={Users} />
            <Route path="/pastries" component={Pastries} />
            <Route path="/orders" component={Orders} />
          </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
