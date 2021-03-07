import './App.css';
import React from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import NavBar from './components/navbar/NavBar';
import ListBlogs from './components/list-blogs/ListBlogs';
import NewBlog from './components/new-blog/NewBlog';
import ShowBlog from './components/show-blog/ShowBlog';

function App() {
  return (
    <Router>
      <NavBar />
      <div className="container-fluid">
        <Switch>
          <Route path="/blog/:blogId">
            <ShowBlog />
          </Route>
          <Route path="/new">
            <NewBlog />
          </Route>
          <Route path="/">
            <ListBlogs />
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
