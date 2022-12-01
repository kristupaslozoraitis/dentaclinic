import { Box, Button, Flex, Table, TableCaption, TableContainer, Tbody, Td, Tfoot, Th, Thead, Tr } from "@chakra-ui/react";
import jwtDecode from "jwt-decode";
import moment from "moment";
import React, { useCallback, useEffect, useState } from "react";
import { FreeVisit } from "../VisitsAdminPanel/types";

interface CardType {
    id: number,
    name: string,
    surname: string,
}

const VisitsHistory = () => {
    const [visits, setVisits] = useState<FreeVisit[]>([]);
    const token = localStorage.getItem("accessToken");
    const userId = jwtDecode(token as string) as any;
    const getVisits = useCallback(async () => {
        const myServices = await fetch(`https://localhost:7257/api/v1/patientCards/${userId.sub}/visits`, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            method: 'GET',
        })
        const allVisits = await myServices.json();
        setVisits(allVisits);
    }, [])

    useEffect(() => {
        getVisits();
    }, [getVisits]);
    return (
        <>
            <TableContainer m={20}>
                <Table variant='simple'>
                    <Thead>
                        <Tr>
                            <Th>Apsilankymo data</Th>
                            <Th>Laikas</Th>
                            <Th>Paslauga</Th>
                            <Th>Odontologas</Th>
                            <Th></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        {visits.map((visit) => {
                            return (
                                <Tr>
                                    <Td>{moment(visit.date).format("YYYY-MM-DD")}</Td>
                                    <Td>{visit.time}</Td>
                                    <Td>{visit.service}</Td>
                                    <Td>{visit.doctorFullName}</Td>
                                    <Td><Button>Atšaukti</Button></Td>
                                </Tr>
                            )
                        })}
                    </Tbody>
                </Table>
            </TableContainer>
        </>
    )
}

export { VisitsHistory }