import react from 'react';
import { BlogPost } from '../../models/Models';
import { Link } from 'react-router-dom';

interface BlogPosts {
    blogPosts: BlogPost[]
}

export default function ListBlogsTable(props: BlogPosts) {
    return(
        <div>
            <div className="row">
                <div className="col"><h1>Blog Listing</h1></div>
            </div>
            <div className="row">
                <div className="col">
                    <table className="table">
                        <thead>
                            <tr>
                                <th>Title</th>
                                <th>Ingress</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {props.blogPosts.map(post => (
                                <tr key={post.id}>
                                    <td>{post.title}</td>
                                    <td>{post.ingress}</td>
                                    <td><Link className="btn btn-primary" to={'/blog/'+post.id}>View</Link></td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}