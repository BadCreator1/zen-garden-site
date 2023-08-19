import { IBlock } from "./IBlock"
import { ICommentary } from "./ICommentary"

export interface IPost {
    id: number
    title: string
    imageUrl: string
    userId?: string
    blocks: IBlock[]
    commentaries: ICommentary[]
    views: number,
    jsonDoc: string,
    description: string

  }