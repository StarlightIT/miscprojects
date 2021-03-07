import React, { useEffect, useState } from 'react';
import {
    useParams,
    useHistory,
  } from 'react-router-dom';
import { BlogPost } from '../../models/Models';

interface BlogId {
    blogId: string;
}

export default function ShowBlog() {
    let { blogId } = useParams<BlogId>();
    let history = useHistory();
    const [blog, setBlog] = useState<BlogPost | undefined>(undefined);
    

    useEffect(() => {
        getBlogEntry();
    });

    async function getBlogEntry() {
        if (!blogId) { 
            history.push('/');
        }
        const response = await fetch('https://localhost:5001/api/BlogPosts/' + blogId);
        const blog = await response.json();
        console.log(blog);
        setBlog(blog);
    }

    if (!blog) {
        return <h1>Loading...</h1>
    }

    let date = new Date(blog.published);

    return (
        <div>
            <div className="row">
                <div className="col">
                    <h1>{blog.title}</h1>
                    <h5>{blog.ingress}</h5>
                    <h5>{blog.author} &lt;{blog.authorEmail}&gt; {date.toLocaleTimeString()} {date.toLocaleDateString()}</h5>
                </div>
            </div>
            <div className="row">
                <div className="col">
                    <p>{blog.text}</p>
                </div>
            </div>
        </div>
    );
}