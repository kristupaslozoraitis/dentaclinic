import { Box, Button, Flex, Select, SimpleGrid } from "@chakra-ui/react";
import React, { useState } from "react";

const FreeVisitsView = () => {
    const [test, setTest] = useState([10, 20, 30, 40]);
    return (
        <Box>
            <Box w={200} mt={5}>
                <Select placeholder="Choose doc">
                    <option value='1'>Gyd1</option>
                    <option value='2'>Gyd2</option>
                    <option value='0'>Any</option>
                </Select>
            </Box>
            <SimpleGrid columns={3} spacing={10} mt={5}>
                {test.map(() => {
                    return (
                        <Flex bg='grey' direction="column">
                            <Flex justifyContent="center">
                                Gydytojas1
                            </Flex>
                            <Flex direction="column" m={3}>
                                <Box>
                                    <Box fontWeight="bold">Data:</Box>
                                    <Box ml={3}>2022-11-25</Box>
                                    <Box fontWeight="bold">Laikas:</Box>
                                    <Box ml={3}>17:30</Box>
                                    <Box fontWeight="bold">Paslauga: </Box>
                                    <Box ml={3}>Burnos higiena</Box>
                                </Box>
                                <Flex justifyContent="end" m={5}>
                                    <Button bg="red.700">Registruotis</Button>
                                </Flex>
                            </Flex>
                        </Flex>
                    )
                })}
            </SimpleGrid>
        </Box>
    )
}

export { FreeVisitsView }