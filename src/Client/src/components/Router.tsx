import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import Home from './Home';
import Login from './authentication/Login';
import Register from './authentication/Register';
import Header from './shared/Header';
import Footer from './shared/Footer';

const AppRouter = (): JSX.Element => {
  return (
    <Router>
      <Header />
      <Switch>
        <Route exact path="/" component={Home} />
        <Route path="/home" component={Home} />
        <Route path="/login" component={Login} />
        <Route path="/register" component={Register} />
      </Switch>
      <Footer />
    </Router>
  );
};

export default AppRouter;