byte[] payload = [0x1, 0xf1, 0xaa, 0xf2];

Span<byte> payloadSpan = payload;

Span<byte> subset = payloadSpan[^2..];

// Range and Slice?

// Index ?
