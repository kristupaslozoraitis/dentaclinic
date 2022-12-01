import { Card, CardBody, CardHeader } from "@chakra-ui/card";
import { Box, Button, Flex, Select, SimpleGrid, Heading, Stack, StackDivider, Text } from "@chakra-ui/react";
import jwtDecode from "jwt-decode";
import moment from "moment";
import React, { useCallback, useEffect, useState } from "react";
import { FreeVisit } from "../VisitsAdminPanel/types";

const FreeVisitsView = () => {
    const [freeVisits, setFreeVisits] = useState<FreeVisit[]>([]);
    const token = localStorage.getItem("accessToken");
    const userId = jwtDecode(token as string) as any;

    const getVisits = useCallback(async () => {
        const myServices = await fetch(`https://localhost:7257/api/v1/freeVisits`, {
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
        getVisits();
    }, [getVisits]);

    const register = async (e: React.MouseEvent<HTMLButtonElement, MouseEvent>, visit: FreeVisit): Promise<void> => {
        const visitDto = {
            time: visit.time,
            date: visit.date,
            doctorName: visit.doctorFullName.split(" ")[0],
            doctorSurname: visit.doctorFullName.split(" ")[1],
            service: visit.service,
            freeVisitId: visit.id
        }
        await fetch(`https://localhost:7257/api/v1/patientCards/${userId.sub}/visits`, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            method: 'POST',
            body: JSON.stringify(visitDto)
        })
    }

    return (
        <Box>
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
                                    <Flex justifyContent='end'>
                                        <Button onClick={(e) => register(e, freeVisit)}>Registruotis</Button>
                                    </Flex>
                                </Stack>
                            </CardBody>
                        </Card>
                    )
                })}
            </SimpleGrid>
        </Box>
    )
}

export { FreeVisitsView }