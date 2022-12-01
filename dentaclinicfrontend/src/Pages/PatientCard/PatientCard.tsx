import { Box, Flex, FormControl, FormLabel, Heading, Input } from "@chakra-ui/react"
import jwtDecode from "jwt-decode";
import moment from "moment";
import React, { useCallback, useEffect, useState } from "react"
import { PatientCardData } from "./types";

const PatientCard = () => {
    const [patientCard, setPatientCard] = useState<PatientCardData>();

    const getVisits = useCallback(async () => {
        var token = localStorage.getItem("accessToken");
        var userId = jwtDecode(token as string) as any;
        const myServices = await fetch(`https://localhost:7257/api/v1/patientsCards/${userId.sub}`, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            method: 'GET',
        })
        const allVisits = await myServices.json();
        setPatientCard(allVisits);
    }, [])

    useEffect(() => {
        getVisits();
    }, [getVisits]);

    return (
        <Box>
            <Box>
                <Heading textAlign="center">
                    Paciento kortelė
                </Heading>
            </Box>
            <FormControl>
                <Flex m={10} direction="row" justifyContent="center">
                    <Flex direction="column" mr={10}>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Vardas</FormLabel>
                            <Input disabled type="text" value={patientCard?.name} />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Pavardė</FormLabel>
                            <Input disabled type="text" value={patientCard?.surname} />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Gimimo data</FormLabel>
                            <Input disabled type="date" value={moment(patientCard?.birthDate).format("YYYY-MM-DD")} />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Asmens kodas</FormLabel>
                            <Input disabled type="text" value={patientCard?.personalNumber} />
                        </Flex>
                    </Flex>
                    <Flex direction="column" ml={10}>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Gyvenamasis adresas</FormLabel>
                            <Input type="text" value={patientCard?.homeAddress} />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Telefonas</FormLabel>
                            <Input type="text" value={patientCard?.phoneNumber} />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Ūgis</FormLabel>
                            <Input type="number" value={patientCard?.height} />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Svoris</FormLabel>
                            <Input type="number" value={patientCard?.weight} />
                        </Flex>
                    </Flex>
                </Flex>
            </FormControl>
        </Box>
    )
}

export { PatientCard }