import { Country } from "./country"

export type City = {
    id: number,
    name: string,
    country: Country,
    latitude: number,
    longitude: number
}

export type CitiesPaginated = {
    items: City[],
    pageIndex: number,
    totalPages: number,
    hasPreviousPage: boolean,
    hasNextPage: boolean
}