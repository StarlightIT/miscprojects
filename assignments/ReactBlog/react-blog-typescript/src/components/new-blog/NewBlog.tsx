import React from "react";
import { useForm } from "react-hook-form";
import { useHistory } from "react-router";
import { BlogPost } from '../../models/Models';

export default function NewBlog() {
    let history = useHistory();
    const { register, setValue, handleSubmit, errors } = useForm<BlogPost>();
    const onSubmit = handleSubmit((blogPost: BlogPost) => {
        console.log(blogPost);
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ 
                title: blogPost.title,
                ingress: blogPost.ingress,
                text: blogPost.text,
                author: blogPost.author,
                authorEmail: blogPost.authorEmail
            })
        };
        fetch('https://localhost:5001/api/BlogPosts', requestOptions)
        .then(response => response.json())
        .then(
            (data: BlogPost) => history.push('/blog/' + data.id)
        );
    });

    return (
        <div>
            <div className="row">
                <div className="col"><h1>New Blog</h1></div>
            </div>
            <form onSubmit={onSubmit}>
                <div className="form-group">
                    <label htmlFor="title">Title</label>
                    <input id="title" name="title" className="form-control" ref={register({ required: true, maxLength: 50 })} />
                </div>
                <div className="form-group">
                    <label htmlFor="ingress">Ingress</label>
                    <input id="ingress" name="ingress" className="form-control" ref={register({ required: true, maxLength: 250 })} />
                </div>
                <div className="form-group">
                    <label htmlFor="text">Text</label>
                    <textarea id="text" name="text" className="form-control" rows={5} cols={20} ref={register({ required: true })} />
                </div>
                <div className="form-group">
                    <label htmlFor="author">Author</label>
                    <input id="author" name="author" className="form-control" ref={register({ required: true, maxLength: 40 })} />
                </div>
                <div className="form-group">
                    <label htmlFor="authorEmail">Email</label>
                    <input id="authorEmail" name="authorEmail" className="form-control" type="email" ref={register({ required: true })} />
                </div>
                <button className="btn btn-primary" type="submit">Create</button>
            </form>
        </div>
    );
} 