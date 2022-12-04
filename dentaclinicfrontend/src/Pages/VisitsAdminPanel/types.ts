export interface Service {
    id: number;
    name: string;
}

export interface FreeVisit{
    date: Date;
    time: string;
    serviceId: number;
    doctorFullName: string;
    service: string;
    id: number;
    patient: string;
}