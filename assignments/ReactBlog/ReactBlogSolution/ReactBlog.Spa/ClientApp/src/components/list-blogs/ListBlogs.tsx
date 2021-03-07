import React from "react";
import { BlogPost } from '../../models/Models';
import ListBlogTable from './ListBlogsTable';

interface ListBlogsProps {
    error: any,
    isLoaded: boolean,
    blogPosts: Array<BlogPost>
}

export default class ListBlogs extends React.Component<any, ListBlogsProps> {
    constructor(props: any) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            blogPosts: []
        }
    }

    componentDidMount() {
        fetch("https://localhost:5001/api/BlogPosts")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        blogPosts: result
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    })
                }
            );
    }

    render() {
        const { error, isLoaded, blogPosts } = this.state;
        if (error) {
          return (
            <div>Error: {error.message}</div>
            );
        } else if (!isLoaded) {
          return <div>Loading...</div>;
        } else {
          return (
              <ListBlogTable blogPosts={blogPosts} />
          );
        }
    } 
} 