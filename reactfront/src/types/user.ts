import { City } from "./city"
import { Level } from "./level"

export type User = {
    userName: string,
    email: string,
    phoneNumber: string,
    gender: string,
    role: string,
    bloodType: number,
    xp: number,
    level: Level,
    city: City,
    givesBlood: boolean
}

export type DoctorFilter = {
    id : string,
    userName: string,
    city: string
}