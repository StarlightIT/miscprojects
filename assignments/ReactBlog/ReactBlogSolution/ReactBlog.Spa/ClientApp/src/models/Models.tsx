interface BlogPost {
    id: string,
    title: string;
    ingress: string,
    text: string,
    author: string,
    authorEmail: string,
    published: Date
}

export type { BlogPost }