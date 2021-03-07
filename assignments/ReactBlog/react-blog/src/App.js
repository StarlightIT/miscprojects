import logo from './logo.svg';
import './App.css';
import React from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  useRouteMatch,
  useParams
} from "react-router-dom";
import ListBlogs from './ListBlogs';
import { Topics, Topic } from './Topics';

function App() {
  return (
    <Router>
      <div>
        <ul>
          <li>
            <Link to="/">List Blogs</Link>
          </li>
          <li>
            <Link to="/about">About</Link>
          </li>
          <li>
            <Link to="/topics">Topics</Link>
          </li>
        </ul>

        <Switch>
          <Route path="/about">
            <About />
          </Route>
          <Route path="/topics">
            <Topics />
          </Route>
          <Route path="/">
            <ListBlogs />
          </Route>
        </Switch>
      </div>
    </Router>
  );
}



function About() {
  return <h2>About</h2>;
}



export default App;
