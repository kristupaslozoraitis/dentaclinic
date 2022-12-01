import { Card, CardHeader, CardBody } from '@chakra-ui/card';
import { CheckIcon, CloseIcon, EditIcon } from '@chakra-ui/icons';
import { Select, SimpleGrid, Flex, Button, Box, Modal, ModalBody, ModalCloseButton, ModalContent, ModalHeader, ModalOverlay, List, ListIcon, ListItem, Heading, Stack, StackDivider, Text, Editable, EditableInput, EditablePreview, Input, ButtonGroup, IconButton, useEditableControls, Divider } from '@chakra-ui/react';
import moment from 'moment';
import React, { FormEvent, useCallback, useEffect, useState } from 'react';
import { LoginForm } from '../../components/LoginForm';
import { NewVisitModal } from './NewVisitModal';
import { ServiceModal } from './ServiceModal';
import { FreeVisit, Service } from './types';

const VisitsAdminPanel = () => {
    const [services, setServices] = useState<Service[]>([]);
    const [freeVisits, setFreeVisits] = useState<FreeVisit[]>([]);
    const [date, setDate] = useState<Date>(new Date());
    const [time, setTime] = useState<string>('');
    const [serviceId, setServiceId] = useState<number>(1);
    const [isOpenServices, setIsOpenServices] = useState(false);
    const [isNewVisitOpen, setIsNewVisitOpen] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [editIndex, setEditIndex] = useState<number>();

    const getServices = useCallback(async () => {
        var token = localStorage.getItem("accessToken");
        const myServices = await fetch("https://localhost:7257/api/v1/services", {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            method: 'GET',
        })
        const allServices = await myServices.json();
        setServices(allServices);
    }, [])

    const getVisits = useCallback(async () => {
        var token = localStorage.getItem("accessToken");
        const myServices = await fetch("https://localhost:7257/api/v1/freeVisits", {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            method: 'GET',
        })
        const allVisits = await myServices.json();
        setFreeVisits(allVisits);
    }, [])

    useEffect(() => {
        getServices();
        getVisits();

        // const data = await fetch("https://localhost:7257/api/me", {
        //     headers: {
        //         'Content-Type': 'application/json',
        //         'Authorization': `Bearer ${token}`
        //     },
        //     method: 'GET',
        // })
        //const roles = await data.json();
        //if (!roles.includes("Odontologist")) {


        //}
    }, [getServices, getVisits]);

    const closeModal = () => {
        setIsOpenServices(false);
    }

    const closeVisitModal = () => {
        setIsNewVisitOpen(false);
    }

    const onSubmit = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
        e.preventDefault();
        var token = localStorage.getItem("accessToken");
        const url = isEditing ? `https://localhost:7257/api/v1/freeVisits/${editIndex}` : "https://localhost:7257/api/v1/freeVisits"
        await fetch(url, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ date, time, serviceId }),
            method: isEditing ? 'PUT' : 'POST',
        })
    }

    const onSave = async (): Promise<void> => {
        var token = localStorage.getItem("accessToken");
        await fetch("https://localhost:7257/api/v1/services", {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(services),
            method: 'PUT',
        })
    }

    const handleServiceChange = (value: string, index: number): void => {
        const tempServices = [...services];
        tempServices[index].name = value;
        setServices(tempServices);
    }

    const handleDateChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
        const value = new Date(e.target.value);
        setDate(value);
    }

    const handleTimeChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
        const value = e.target.value;
        setTime(value);
    }

    const handleServiceIdChange = (e: React.ChangeEvent<HTMLSelectElement>): void => {
        const value = e.target.value as unknown as number;
        setServiceId(value);
    }

    const editVisit = (index:number):void => {
        setIsEditing(true);
        const editingVisit = freeVisits[index];
        setDate(editingVisit.date);
        setEditIndex(editingVisit.id);
        setTime(editingVisit.time);
        setServiceId(editingVisit.serviceId);
        setIsNewVisitOpen(true);
    }

    return (
        <>
            <Box m={5}>
                <Flex gap={10} mt={5} justifyContent='end'>
                    <Button onClick={() => setIsOpenServices(true)}>Mano paslaugos</Button>
                    <Button onClick={() => setIsNewVisitOpen(true)}>Pridėti vizitą</Button>
                </Flex>
                <SimpleGrid columns={3} spacing={10} mt={5}>
                    {freeVisits.map((freeVisit, index) => {
                        return (
                            <Card key={index} border='solid' borderRadius={10}>
                                <CardHeader m={5} textAlign='center'>
                                    <Heading size='md'>{freeVisit.doctorFullName}</Heading>
                                </CardHeader>

                                <CardBody m={5}>
                                    <Stack divider={<StackDivider />} spacing='4'>
                                        <Box>
                                            <Heading size='xs' textTransform='uppercase'>
                                                Data
                                            </Heading>
                                            <Text pt='2' fontSize='sm'>
                                                {moment(freeVisit.date).format("YYYY-MM-DD")}
                                            </Text>
                                        </Box>
                                        <Box>
                                            <Heading size='xs' textTransform='uppercase'>
                                                Laikas
                                            </Heading>
                                            <Text pt='2' fontSize='sm'>
                                                {freeVisit.time}
                                            </Text>
                                        </Box>
                                        <Box>
                                            <Heading size='xs' textTransform='uppercase'>
                                                Paslauga
                                            </Heading>
                                            <Text pt='2' fontSize='sm'>
                                                {freeVisit.service}
                                            </Text>
                                        </Box>
                                        <Flex textAlign='right' justifyContent='space-between' alignItems='center'>
                                            <Text fontSize='lg'>
                                                Pacientas
                                            </Text>
                                            <Button onClick={() => editVisit(index)}>Redaguoti vizitą</Button>
                                        </Flex>
                                    </Stack>
                                </CardBody>
                            </Card>
                        )
                    })}
                </SimpleGrid>
            </Box>
            <ServiceModal isOpen={isOpenServices} closeModal={closeModal} services={services} save={onSave} handleServiceChange={handleServiceChange} />
            <NewVisitModal
                isOpen={isNewVisitOpen}
                closeModal={closeVisitModal}
                save={onSubmit}
                services={services}
                handleDateChange={handleDateChange}
                handleTimeChange={handleTimeChange}
                handleServiceIdChange={handleServiceIdChange}
                date={date}
                time={time}
                serviceId={serviceId}
            />
        </>
    )
}

export { VisitsAdminPanel }