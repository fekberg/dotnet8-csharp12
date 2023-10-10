byte[] payload = new byte[] { 0x1, 0xf1, 0xaa, 0xf2 };

Memory<byte> payloadSpan = payload;

Memory<byte> subset = payloadSpan[^2..];


// Range and Slice?

// Index ?
