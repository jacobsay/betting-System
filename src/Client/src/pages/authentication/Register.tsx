import React, { useContext, useEffect, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';

import { map, mergeMap } from 'rxjs';

import { AuthenticationContext } from '../../components/contexts/ContextWrapper';
import ICredentials from '../../models/credentials';
import IGambler from '../../models/gambler';
import errorsService from '../../services/errorsService';
import gamblerService from '../../services/gamblerService';
import jwtService from '../../services/jwtService';
import usersService from '../../services/usersService';

const Register = (): JSX.Element => {
  const history = useHistory();
  const { isAuthenticated, setIsAuthenticated, setGamblerId } = useContext(AuthenticationContext);

  const [credentials, setCredentials] = useState<ICredentials & IGambler>({
    name: '',
    email: '',
    password: '',
    confirmPassword: ''
  });

  useEffect(() => {
    if (isAuthenticated) {
      history.push('/');
    }
  }, []);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCredentials({
      ...credentials,
      [event.target.name]: event.target.value
    });
  };

  const register = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    usersService
      .register(credentials)
      .pipe(
        mergeMap(_ =>
          usersService.login(credentials).pipe(
            map(res => {
              jwtService.saveToken(res.data.token);
              setIsAuthenticated(true);
            })
          )
        ),
        mergeMap(_ =>
          gamblerService.create({ name: credentials.name }).pipe(
            map(res => {
              setGamblerId(res.data.gamblerId);
              gamblerService.setIdInLocalStorage(res.data.id);
            })
          )
        )
      )
      .subscribe({
        next: _ => history.push('/'),
        error: errorsService.handle
      });
  };

  return (
    <div className="col-xl-8 col-lg-8 col-md-6 col-sm-6 mt-40">
      <div className="content-center cl-white">
        <div className="row justify-content-center">
          <div className="col-6">
            <div className="account-form">
              <div className="title">
                <h3>Create your account</h3>
              </div>
              <div className="via-login">
                <a href="" className="facebook-bg">
                  <i className="fa fa-facebook"></i>Signup with Facebook
                </a>
                <a href="" className="google-plus-bg">
                  <i className="fa fa-google"></i>Signup with Google
                </a>
              </div>
              <form action="POST" onSubmit={register}>
                <div className="row">
                  <div className="col-xl-12">
                    <input
                      type="text"
                      name="name"
                      value={credentials.name}
                      onChange={handleChange}
                      placeholder="First and Last Name"
                    />
                  </div>
                  <div className="col-xl-12">
                    <input
                      type="email"
                      name="email"
                      value={credentials.email}
                      onChange={handleChange}
                      placeholder="Email"
                    />
                  </div>
                  <div className="col-xl-12">
                    <input
                      type="password"
                      name="password"
                      value={credentials.password}
                      onChange={handleChange}
                      placeholder="Password"
                    />
                  </div>
                  <div className="col-xl-12">
                    <input
                      type="password"
                      name="confirmPassword"
                      value={credentials.confirmPassword}
                      onChange={handleChange}
                      placeholder="Confirm Password"
                    />
                  </div>
                  <div className="col-xl-12">
                    <p>
                      By signing up to betsb you confirm that you agree with the{' '}
                      <a href="">member terms and conditions</a>
                    </p>
                  </div>
                  <div className="col-xl-12">
                    <button type="submit" className="bttn-small btn-fill w-100">
                      Create my account
                    </button>
                  </div>
                  <div className="col-xl-12">
                    <p>
                      <Link to="/login">Do you already have an account?</Link>
                    </p>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
