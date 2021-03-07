import React from "react";
import { useForm } from "react-hook-form";
import { ErrorMessage } from '@hookform/error-message';
import { useHistory } from "react-router";
import { BlogPost } from '../../models/Models';

export default function NewBlog() {
    let history = useHistory();
    let red = { color: "red" };
    const { register, setValue, handleSubmit, errors } = useForm<BlogPost>();
    const onSubmit = handleSubmit((blogPost: BlogPost) => {
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
                    <input id="title" name="title" className="form-control" ref={register({ required: 'This is required', maxLength: {
                        value: 50,
                        message: 'This input exceed max length',
                    } })} />
                    <ErrorMessage errors={errors} name="title" render={({ message }) => <p style={red}>{message}</p>} />
                </div>
                <div className="form-group">
                    <label htmlFor="ingress">Ingress</label>
                    <input id="ingress" name="ingress" className="form-control" ref={register({ required: 'This is required', maxLength: {
                        value: 250,
                        message: 'This input exceed max length',
                    } })} />
                    <ErrorMessage errors={errors} name="ingress" render={({ message }) => <p style={red}>{message}</p>} />
                </div>
                <div className="form-group">
                    <label htmlFor="text">Text</label>
                    <textarea id="text" name="text" className="form-control" rows={5} cols={20} ref={register({ required: 'This is required' })} />
                    <ErrorMessage errors={errors} name="text" render={({ message }) => <p style={red}>{message}</p>} />
                </div>
                <div className="form-group">
                    <label htmlFor="author">Author</label>
                    <input id="author" name="author" className="form-control" ref={register({ required: 'This is required', maxLength: {
                        value: 40,
                        message: 'This input exceed max length',
                    } })} />
                    <ErrorMessage errors={errors} name="author" render={({ message }) => <p style={red}>{message}</p>} />
                </div>
                <div className="form-group">
                    <label htmlFor="authorEmail">Email</label>
                    <input id="authorEmail" name="authorEmail" className="form-control" type="email" ref={register({ required: 'This is required' })} />
                    <ErrorMessage errors={errors} name="authorEmail" render={({ message }) => <p style={red}>{message}</p>} />
                </div>
                <button className="btn btn-primary" type="submit">Create</button>
            </form>
        </div>
    );
} 